import { Request, Response, NextFunction } from 'express';

export default interface IPostController {
    createPost(req: Request, res: Response, next: NextFunction);
    getPostsByUserId(req: Request, res: Response, next: NextFunction);
    likeOrDislikePost(req: Request, res: Response, next: NextFunction);
    deleteUserPosts(req: Request, res: Response, next: NextFunction);
    getPostsConectionByUserId(req: Request, res: Response, next: NextFunction);
    getPostById(req: Request, res: Response, next: NextFunction);
}
