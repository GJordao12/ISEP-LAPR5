import { Mapper } from '../core/infra/Mapper';

import { Document, Model } from 'mongoose';

import { UniqueEntityID } from '../core/domain/UniqueEntityID';
import { Post } from '../domain/post';
import IPostDTO from '../dto/IPostDTO';
import {IPostPersistence} from "../dataschema/IPostPersistence";

export class PostMap extends Mapper<Post> {
    public static toDTO(post: Post): IPostDTO {
    // eslint-disable-next-line @typescript-eslint/no-object-literal-type-assertion
        return {
            id: post.id.toString(),
            idUser: post.idUser,
            likes: post.likes,
            dislikes: post.dislikes,
            texto: post.texto.props.value,
            listTags: post.listTags,
            data: post.data,
            listComentarios: post.listComentario,
        } as  IPostDTO;
    }

    public static toDomain(post: any | Model<IPostPersistence & Document>): Post {
        const postOrError = Post.create1(post, new UniqueEntityID(post.domainId));

        postOrError.isFailure ? console.log(postOrError.error) : '';

        return postOrError.isSuccess ? postOrError.getValue() : null;
    }

    public static toPersistence(post: Post): any {
        return {
            domainId: post.id.toString(),
            idUser: post.idUser,
            likes: post.likes,
            dislikes: post.dislikes,
            texto: post.texto.props.value,
            listTags: post.listTags,
            data: post.data,
            listComentarios:post.listComentario,
        };
    }
}
