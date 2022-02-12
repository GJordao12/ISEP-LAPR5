import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from '../../config';

import IPostService from '../services/IServices/IPostService';
import IPostDTO from '../dto/IPostDTO';

import { Result } from '../core/logic/Result';
import IPostController from './IControllers/IPostController';

@Service()
export default class PostController implements IPostController /* TODO: extends ../core/infra/BaseController */ {

    constructor(@Inject(config.services.post.name) private postServiceInstance: IPostService) {}

    /**
    *
    * CreatePost Method to Create a Post
    *
    * @param req
    * @param res
    * @param next
    */
    public async createPost(req: Request, res: Response, next: NextFunction) {
        try {
            const postOrError = (await this.postServiceInstance.createPost(await this.postServiceInstance.updateTags(req.body as IPostDTO))) as Result<IPostDTO>;

            if (postOrError.isFailure) {
                return res.status(402).send();
            }

            const postDTO = postOrError.getValue();
            return res.json(postDTO).status(201);
        } catch (e) {
            return next(e);
        }
    }

    /**
   *
     *
     * GetPostsByUserId Method to obtain Posts from User with ID specified
     *
   * @param req
   * @param res
   * @param next
   */
    public async getPostsByUserId(req: Request, res: Response, next: NextFunction) {
        try {
            let aux = req.url.substring(1,req.url.length);
            const postOrError = (await this.postServiceInstance.getPostsByUserId(aux)) as Result<IPostDTO[]>;

            if (postOrError.isFailure) {
                return res.status(402).send();
            }

            const postDTO = postOrError.getValue();
            return res.json(postDTO).status(201);
        } catch (e) {
            return next(e);
        }
    }

    /**
     *
     * GetPostsConectionByUserId Method
     *
     * @param req
     * @param res
     * @param next
     */
    public async getPostsConectionByUserId(req: Request, res: Response, next: NextFunction) {
        try {
            let aux = req.url.substring(7,req.url.length);
            const postOrError = (await this.postServiceInstance.getPostsConectionByUserId(aux)) as Result<IPostDTO[]>;

            if (postOrError.isFailure) {
                return res.status(402).send();
            }

            const postDTO = postOrError.getValue();
            return res.json(postDTO).status(201);
        } catch (e) {
            return next(e);
        }
    }

    /**
     *
     * LikeOrDislikePost Method to Like or Dislike a Post
     *
     * @param req
     * @param res
     * @param next
     */
    public async likeOrDislikePost(req: Request, res: Response, next: NextFunction){

        try{
            const postOrError = await this.postServiceInstance.updateLikeOrDislike(
                req.body['postId'],
                req.body['status'],
                req.body['userId'],
            );
            if (postOrError.isFailure) {
                return res.status(402).send();
            }

            return res.json(postOrError.getValue()).status(200);

        }catch (e){
            return next(e);
        }
    }

    /**
     *
     * DeleteUserPosts Method to Delete User Posts
     *
     * @param req
     * @param res
     * @param next
     */
    public async deleteUserPosts(req: Request, res: Response, next: NextFunction) {
        try {
            let aux = req.url.substring(1,req.url.length);
            await this.postServiceInstance.deleteUserPosts(aux);

            return res.json().status(201);
        } catch (e) {
            return next(e);
        }
    }

  public async getPostById(req: Request, res: Response, next: NextFunction) {
    try {
      let aux = req.url.substring(6,req.url.length);

      const postOrError = (await this.postServiceInstance.getPost(aux)) as Result<IPostDTO>;

      if (postOrError.isFailure) {
        return res.status(402).send();
      }

      const postDTO = postOrError.getValue();
      return res.json(postDTO).status(201);
    } catch (e) {
      return next(e);
    }
  }
}
