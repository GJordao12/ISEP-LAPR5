import { Result } from '../../core/logic/Result';
import IPostDTO from '../../dto/IPostDTO';
import IComentarioDTO from '../../dto/IComentarioDTO';

export default interface IPostService {
    createPost(postDTO: IPostDTO): Promise<Result<IPostDTO>>;
    getPost(postId: string): Promise<Result<IPostDTO>>;
    getPostsByUserId(userId: string): Promise<Result<IPostDTO[]>>;
    updatePost(postDTO: IPostDTO): Promise<Result<IPostDTO>>;
    updatePostLikes(comentarioDTO: IComentarioDTO): Promise<Result<IPostDTO>>;
    updateLikeOrDislike(postId: string, status: string, userId: string): Promise<Result<IPostDTO>>;
    updateTags(post: IPostDTO): Promise<IPostDTO>;
    deleteUserPosts(aux: string);
    getPostsConectionByUserId(userId: string): Promise<Result<IPostDTO[]>>;
}
