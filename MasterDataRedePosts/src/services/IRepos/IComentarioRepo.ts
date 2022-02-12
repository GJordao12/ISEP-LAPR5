import { Repo } from '../../core/infra/Repo';
import { Comentario} from "../../domain/comentario";
import { ComentarioId} from "../../domain/comentarioId";

export default interface IComentarioRepo extends Repo<Comentario>{
    save(comentario: Comentario): Promise<Comentario>;
    findByDomainId(comentarioID: ComentarioId | string): Promise<Comentario>;
    deleteByUserId(userId: string);
}
