import { Comentario } from '../domain/comentario';

export interface IPostPersistence {
    domainId: string;
    idUser: string;
    likes: string[];
    dislikes: string[];
    texto: string;
    listTags: string[];
    data: Date;
   listComentarios: Comentario[];
}
