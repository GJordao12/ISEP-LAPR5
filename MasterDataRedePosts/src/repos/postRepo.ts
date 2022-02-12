import { Service, Inject } from 'typedi';

import { Document, FilterQuery, Model } from 'mongoose';

import IPostRepo from '../services/IRepos/IPostRepo';
import { IPostPersistence } from '../dataschema/IPostPersistence';
import { PostId } from '../domain/postId';
import { Post } from '../domain/post';
import { PostMap } from '../mappers/PostMap';

@Service()
export default class PostRepo implements IPostRepo {
    private models: any;

    constructor(@Inject('postSchema') private postSchema: Model<IPostPersistence & Document>) {}

    private createBaseQuery(): any {
        return {
            where: {},
        };
    }

    public async exists(post: Post): Promise<boolean> {
    // eslint-disable-next-line @typescript-eslint/no-angle-bracket-type-assertion
        const idX = post.id instanceof PostId ? (<PostId>post.id).id : post.id;

        const query = { domainId: idX };
        const postDocument = await this.postSchema.findOne(query as FilterQuery<IPostPersistence & Document>);

        return !!postDocument === true;
    }

    public async save(post: Post): Promise<Post> {
        const query = { domainId: post.id.toString() };

        const postDocument = await this.postSchema.findOne(query);

        try {
            if (postDocument === null) {
                const rawUser: any = PostMap.toPersistence(post);

                const postCreated = await this.postSchema.create(rawUser);

                return PostMap.toDomain(postCreated);
            } else {
                postDocument.id = post.id.toString();
                postDocument.idUser = post.idUser;
                postDocument.likes = post.likes;
                postDocument.dislikes = post.dislikes;
                postDocument.texto = post.texto.props.value;
                postDocument.listTags = post.listTags;
                postDocument.data=post.data;
                // @ts-ignore
                postDocument.listComentarios=post.listComentario;
                await postDocument.save();

                return post;
            }
        } catch (err) {
            throw err;
        }
    }

    public async findByDomainId(postId: PostId | string): Promise<Post> {
        const query = { domainId: postId };
        const postRecord = await this.postSchema.findOne(query as FilterQuery<IPostPersistence & Document>);

        if (postRecord != null) {
            return PostMap.toDomain(postRecord);
        } else return null;
    }

    public async findByUserId(userID: PostId | string): Promise<Post[]> {
        const query = { idUser: userID};
        const postRecord = await this.postSchema.find(query as FilterQuery<IPostPersistence & Document>);
        if (postRecord != null) {
            let array: Post[] = [];
            postRecord.forEach( (element) => {
                array.push(PostMap.toDomain(element));
            });
            return array;
        } else return null;
    }

    public async deleteByUserId(userID: PostId | string) {
        const query = { idUser: userID};
        await this.postSchema.deleteMany(query as FilterQuery<IPostPersistence & Document>);
    }
}
