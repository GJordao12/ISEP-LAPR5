export default interface IComentarioDTO{
    id: string;
    idPost: string;
    idUser: string;
    texto: string;
    listTags: string[];
    likes: string[];
    dislikes: string[];
    data: Date;
}
