export interface IComentarioPersistence{
    domainId: string;
    idPost: string;
    idUser: string;
    texto: string;
    listTags: string[];
    likes: string[];
    dislikes: string[];
    data: Date;
}
