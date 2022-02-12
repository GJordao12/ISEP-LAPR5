import { AggregateRoot } from "../core/domain/AggregateRoot";
import { UniqueEntityID } from "../core/domain/UniqueEntityID";

import { Result } from "../core/logic/Result";
import { PostId } from "./postId";
import { TextoPost } from "./textoPost";

import IPostDTO from "../dto/IPostDTO";
import { Comentario } from "./comentario";
import IComentarioDTO from "../dto/IComentarioDTO";

interface PostProps {
    idUser: string;
    likes: string[];
    dislikes: string[];
    texto: TextoPost;
    listTags: string[];
    data: Date;
    listComentarios: IComentarioDTO[];
}

export class Post extends AggregateRoot<PostProps> {
    get id(): UniqueEntityID {
        return this._id;
    }

    get postId(): PostId {
        return new PostId(this.postId.id);
    }

    get idUser(): string {
        return this.props.idUser;
    }

    set idUser(value: string) {
        this.props.idUser = value;
    }

    get likes(): string[] {
        return this.props.likes;
    }

    set likes(value: string[]) {
        this.props.likes = value;
    }

    get dislikes(): string[] {
        return this.props.dislikes;
    }

    set dislikes(value: string[]) {
        this.props.dislikes = value;
    }

    get texto(): TextoPost {
        return this.props.texto;
    }

    set texto(value: TextoPost) {
        this.props.texto = value;
    }

    get listTags(): string[] {
        return this.props.listTags;
    }

    set listTags(value: string[]) {
        this.props.listTags = value;
    }

    get data(): Date {
        return this.props.data;
    }

    set data(value: Date) {
        this.props.data = value;
    }

    get listComentario(): IComentarioDTO[] {
        return this.props.listComentarios;
    }
    set listComentario(value: IComentarioDTO[]) {
        this.props.listComentarios = value;
    }

    private constructor(props: PostProps, id?: UniqueEntityID) {
        super(props, id);
    }

    public static create(postDTO: IPostDTO, id?: UniqueEntityID): Result<Post> {
        const post = new Post(
            {
                idUser: postDTO.idUser,
                likes: postDTO.likes,
                dislikes: postDTO.dislikes,
                texto: new TextoPost({ value: postDTO.texto }),
                listTags: postDTO.listTags,
                data: new Date(),
                listComentarios: postDTO.listComentarios,
            },
            id
        );
        return Result.ok<Post>(post);
    }

    public static create1(postDTO: IPostDTO, id?: UniqueEntityID): Result<Post> {
        const post = new Post(
            {
                idUser: postDTO.idUser,
                likes: postDTO.likes,
                dislikes: postDTO.dislikes,
                texto: new TextoPost({ value: postDTO.texto }),
                listTags: postDTO.listTags,
                data: postDTO.data,
                listComentarios: postDTO.listComentarios,
            },
            id
        );
        return Result.ok<Post>(post);
    }
}
