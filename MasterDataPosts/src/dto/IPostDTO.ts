import IComentarioDTO from "./IComentarioDTO";

export default interface IPostDTO {
    id: string;
    idUser: string;
    likes: string[];
    dislikes: string[];
    texto: string;
    listTags: string[];
    data: Date;
    listComentarios: IComentarioDTO[];
}
