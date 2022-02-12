import { Result } from "../../core/logic/Result";
import IComentarioDTO from "../../dto/IComentarioDTO";

export default interface IComentarioService{
    createComentario(comentarioDTO: IComentarioDTO): Promise<Result<IComentarioDTO>>;
    getComentario(comentarioID: string): Promise<Result<IComentarioDTO>>;
    updateTags(comentario: IComentarioDTO): Promise<IComentarioDTO>;
    updateLikeOrDislike(comentarioId: string, status: string, userId: string): Promise<Result<IComentarioDTO>>;
    deleteUserComments(aux: string);
}
