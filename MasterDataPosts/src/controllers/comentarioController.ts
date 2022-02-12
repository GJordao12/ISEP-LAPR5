import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from '../../config';

import IComentarioService from "../services/IServices/IComentarioService";
import IComentarioDTO from "../dto/IComentarioDTO";

import { Result } from '../core/logic/Result';
import IComentarioController from "./IControllers/IComentarioController";
import IPostService from "../services/IServices/IPostService";

@Service()
export default class ComentarioController implements IComentarioController {

    constructor(@Inject(config.services.comentario.name) private comentarioServiceInstance: IComentarioService, @Inject(config.services.post.name) private postServiceInstance: IPostService) {}

    public async createComentario(req: Request, res: Response, next: NextFunction){

        try {
            const comentarioOrError = (await this.comentarioServiceInstance.createComentario(await this.comentarioServiceInstance.updateTags(req.body as IComentarioDTO))) as Result<IComentarioDTO>;

            if (comentarioOrError.isFailure){
                return res.status(400).send();
            }

            const postDTO = await this.postServiceInstance.getPost(comentarioOrError.getValue().idPost);
            const comentarioDTO = comentarioOrError.getValue();

            postDTO.getValue().listComentarios.push(comentarioOrError.getValue())
            await this.postServiceInstance.updatePost(postDTO.getValue());

            return res.json(comentarioDTO).status(201);
        }
        catch (e){
            return next(e);
        }
    }

    public async likeOrDislikeComentario(req: Request, res: Response, next: NextFunction){

        try{
            const comentarioOrError = await this.comentarioServiceInstance.updateLikeOrDislike(
                req.body['comentarioId'],
                req.body['status'],
                req.body['userId'],
            );
            if (comentarioOrError.isFailure) {
                return res.status(402).send();
            }

            const comentarioDTO = await this.comentarioServiceInstance.getComentario(req.body['comentarioId']);

            await this.postServiceInstance.updatePostLikes(comentarioDTO.getValue());

            return res.json(comentarioOrError.getValue()).status(200);

        }catch (e){
            return next(e);
        }
    }

    public async deleteUserComments(req: Request, res: Response, next: NextFunction) {
        try {
            let aux = req.url.substring(1,req.url.length);
            await this.comentarioServiceInstance.deleteUserComments(aux);

            return res.json().status(201);
        } catch (e) {
            return next(e);
        }
    }
}
