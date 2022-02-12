import { AggregateRoot } from '../core/domain/AggregateRoot';
import { UniqueEntityID } from '../core/domain/UniqueEntityID';

import { Result } from '../core/logic/Result';
import { ComentarioId } from "./comentarioId";
import { TextoComentario } from "./textoComentario";

import IComentarioDTO from "../dto/IComentarioDTO";

interface ComentarioProps {
    idUser: string;
    idPost: string;
    texto: TextoComentario;
    listTags: string[];
    likes: string[];
    dislikes: string[];
    data: Date;
}

export class Comentario extends AggregateRoot<ComentarioProps> {

    get id(): UniqueEntityID{
        return this._id;
    }

    get comentarioId(): ComentarioId{
        return new ComentarioId(this.comentarioId.id);
    }

    get idUser(): string{
        return this.props.idUser;
    }

    set idUser(value: string){
        this.props.idUser = value;
    }

    get idPost(): string{
        return this.props.idPost;
    }

    set idPost(value: string){
        this.props.idPost = value;
    }

    get texto(): TextoComentario {
        return this.props.texto;
    }

    set texto(value: TextoComentario) {
        this.props.texto = value;
    }

    get listTags(): string[] {
        return this.props.listTags;
    }

    set listTags(value: string[]) {
        this.props.listTags = value;
    }

    get likes(): string[]{
        return this.props.likes;
    }

    set likes(value: string[]){
        this.props.likes = value;
    }

    get dislikes(): string[]{
        return this.props.dislikes;
    }

    set dislikes(value: string[]){
        this.props.dislikes = value;
    }

    get data(): Date{
        return this.props.data;
    }

    set data(value: Date){
        this.props.data = value;
    }

    private constructor(props: ComentarioProps, id?: UniqueEntityID) {
        super(props,id);
    }

    public static create(comentarioDTO: IComentarioDTO, id?: UniqueEntityID): Result<Comentario>{

        const likes2 = comentarioDTO.likes === undefined ? new Array<string>() : comentarioDTO.likes;
        const dislikes2 = comentarioDTO.dislikes === undefined ? new Array<string>() : comentarioDTO.dislikes;

        const comentario = new Comentario(
            {
                idUser: comentarioDTO.idUser,
                idPost: comentarioDTO.idPost,
                texto: new TextoComentario({value: comentarioDTO.texto}),
                listTags: comentarioDTO.listTags,
                likes: likes2,
                dislikes: dislikes2,
                data: new Date(),
            }, id,);
        return Result.ok<Comentario>(comentario);
    }
}
