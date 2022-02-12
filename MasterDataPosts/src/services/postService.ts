import {Service, Inject} from 'typedi';
import config from '../../config';
import IPostDTO from '../dto/IPostDTO';
import {Post} from '../domain/post';
import IPostRepo from '../services/IRepos/IPostRepo';
import IPostService from './IServices/IPostService';
import {Result} from '../core/logic/Result';
import {PostMap} from '../mappers/PostMap';
import {Collection} from "mongodb";
import {Comentario} from "../domain/comentario";
import {UniqueEntityID} from "../core/domain/UniqueEntityID";
import {ComentarioMap} from '../mappers/ComentarioMap';
import IComentarioDTO from '../dto/IComentarioDTO';
import https = require('https');
import fetch = require('node-fetch');

@Service()
export default class PostService implements IPostService {
    constructor(@Inject(config.repos.post.name) private postRepo: IPostRepo) {
    }

    httpsAgent = new https.Agent({
        rejectUnauthorized: false,
    });

    public async getPost(postId: string): Promise<Result<IPostDTO>> {
        try {
            const post = await this.postRepo.findByDomainId(postId);
            if (post === null) {
                return Result.fail<IPostDTO>('Post not found');
            } else {
                const postDTOResult = PostMap.toDTO(post) as IPostDTO;
                return Result.ok<IPostDTO>(postDTOResult);
            }
        } catch (e) {
            throw e;
        }
    }

    public async getPostsByUserId(userId: string): Promise<Result<IPostDTO[]>> {
        try {
            const posts = await this.postRepo.findByUserId(userId);
            if (posts === null) {
                return Result.fail<IPostDTO[]>('Post not found');
            } else {
                let array: IPostDTO[] = [];
                posts.forEach((element) => {
                    array.push(PostMap.toDTO(element));
                });
                return Result.ok<IPostDTO[]>(array);
            }
        } catch (e) {
            throw e;
        }
    }
    public async getPostsConectionByUserId(userId: string): Promise<Result<IPostDTO[]>> {

        const response = await fetch("https://localhost:5001/api/Ligacao/user=".concat(userId), {
            method: 'GET',
            agent: this.httpsAgent,
        });
        let json = await response.json();
        let array: IPostDTO[] = [];
        for ( let element in json)  {
            const posts = await this.postRepo.findByUserId(json[element].secundario.value);
            posts.forEach( (element1) => {
                array.push(PostMap.toDTO(element1));
            });
        };
        const posts = await this.postRepo.findByUserId(userId);
        posts.forEach( (element1) => {
            array.push(PostMap.toDTO(element1));
        });
        return Result.ok<IPostDTO[]>(array);

    }

    public async deleteUserPosts(userId: string) {
        try {
            await this.postRepo.deleteByUserId(userId);
        } catch (e) {
            throw e;
        }
    }


    public async createPost(postDTO: IPostDTO): Promise<Result<IPostDTO>> {
        try {
            const postOrError = await Post.create(postDTO);

            if (postOrError.isFailure) {
                return Result.fail<IPostDTO>(postOrError.errorValue());
            }

            const postResult = postOrError.getValue();

            await this.postRepo.save(postResult);

            const postDTOResult = PostMap.toDTO(postResult) as IPostDTO;
            return Result.ok<IPostDTO>(postDTOResult);
        } catch (e) {
            throw e;
        }
    }

    public async updatePost(postDTO: IPostDTO): Promise<Result<IPostDTO>> {
        try {
            const post = await this.postRepo.findByDomainId(postDTO.id);

            if (post === null) {
                return Result.fail<IPostDTO>('Post not found');
            } else {

                var comentarioDTO = postDTO.listComentarios.pop();

                post.listComentario.push(ComentarioMap.toDTO(Comentario.create(comentarioDTO, new UniqueEntityID(comentarioDTO.id)).getValue()));
                await this.postRepo.save(post);
            }
        } catch (e) {
            throw e;
        }
    }

    public async updatePostLikes(comentarioDTO: IComentarioDTO): Promise<Result<IPostDTO>> {

        try {
            const post = await this.postRepo.findByDomainId(comentarioDTO.idPost);

            if (post === null) {
                return Result.fail<IPostDTO>('Post not found');
            } else {
                var removeIndex = post.listComentario.map(item => item.id).indexOf(comentarioDTO.id);

                ~removeIndex && post.listComentario.splice(removeIndex, 1);

                post.listComentario.push(comentarioDTO);
                await this.postRepo.save(post);
            }

        } catch (e) {
            throw e;
        }
    }

    public async updateTags(post: IPostDTO): Promise<IPostDTO> {
        const tags = new Array<string>();
        for (const name of post.listTags) {
            const response = await fetch("https://localhost:5001/api/Tag/AddTags/".concat(name), {
                method: 'PUT',
                agent: this.httpsAgent,
            });
            const data = await response.json();
            tags.push(data.id);
        }

        post.listTags = tags;
        return post;
    }

    public async updateLikeOrDislike(postId: string, status: string, userId: string): Promise<Result<IPostDTO>> {
        try {
            const post = await this.postRepo.findByDomainId(postId);
            if (post === null) {
                return Result.fail<IPostDTO>('Comment not found');
            } else {
                if (status === 'like') {
                    if (!post.likes.includes(userId) && !post.dislikes.includes(userId)) {
                        post.likes.push(userId);
                    }
                } else if (status === 'dislike') {
                    if (!post.likes.includes(userId) && !post.dislikes.includes(userId)) {
                        post.dislikes.push(userId);
                    }
                }
                await this.updateForcaRelacao(userId, post.idUser, status);
                await this.postRepo.save(post);
                const postDTOResult = PostMap.toDTO(post) as IPostDTO;
                return Result.ok<IPostDTO>(postDTOResult);
            }
        } catch (e) {
            throw (e);
        }
    }

    public async updateForcaRelacao(idRemetente: string, idDestinatario: string, status: string) {
        const response = await fetch("https://localhost:5001/api/Ligacao/ForcaRelacao/" + idRemetente + "/" + idDestinatario + "/" + status, {
            method: 'PUT',
            agent: this.httpsAgent,
        })
    }
}
