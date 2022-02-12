import { Repo } from '../../core/infra/Repo';
import { Post } from '../../domain/post';
import { PostId } from '../../domain/postId';

export default interface IPostRepo extends Repo<Post> {
    save(post: Post): Promise<Post>;
    findByDomainId(postId: PostId | string): Promise<Post>;
    findByUserId(userId: PostId | string): Promise<Post[]>;
    deleteByUserId(userId: string);
}
