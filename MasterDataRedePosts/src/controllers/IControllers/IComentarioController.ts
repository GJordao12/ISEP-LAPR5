import { Request, Response, NextFunction} from "express";

export default interface IComentarioController{
    createComentario(req: Request, res: Response, next: NextFunction);
    likeOrDislikeComentario(req: Request, res: Response, next: NextFunction);
    deleteUserComments(req: Request, res: Response, next: NextFunction);
}
