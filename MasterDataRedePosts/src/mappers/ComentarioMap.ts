import {Mapper} from '../core/infra/Mapper';

import {Document, Model} from "mongoose";

import {UniqueEntityID} from "../core/domain/UniqueEntityID";
import {Comentario} from "../domain/comentario";
import IComentarioDTO from "../dto/IComentarioDTO";
import {IComentarioPersistence} from "../dataschema/IComentarioPersistence";

export class ComentarioMap extends Mapper<Comentario>
{
    public static toDTO(comentario: Comentario): IComentarioDTO{
        // eslint-disable-next-line @typescript-eslint/no-object-literal-type-assertion
        return {
            id: comentario.id.toString(),
            idUser: comentario.idUser,
            idPost: comentario.idPost,
            texto: comentario.texto.props.value,
            listTags: comentario.listTags,
            likes: comentario.likes,
            dislikes: comentario.dislikes,
            data: comentario.data,
        } as IComentarioDTO;
    }

    public static toDomain(comentario: any | Model<IComentarioPersistence & Document>): Comentario {

        const comentarioOrError = Comentario.create(comentario,new UniqueEntityID(comentario.domainId));

        comentarioOrError.isFailure ? console.log(comentarioOrError.error) : '';

        return comentarioOrError.isSuccess ? comentarioOrError.getValue() : null;
    }

    public static toPersistence(comentario: Comentario): any{
        return {
            domainId: comentario.id.toString(),
            idUser: comentario.idUser,
            idPost: comentario.idPost.toString(),
            texto: comentario.texto.props.value,
            listTags: comentario.listTags,
            likes: comentario.likes,
            dislikes: comentario.dislikes,
            data: comentario.data,
        };
    }

}
