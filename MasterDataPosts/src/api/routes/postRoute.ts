import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IPostController from '../../controllers/IControllers/IPostController';

import config from '../../../config';

const route = Router();

export default (app: Router) => {
    app.use('/post', route);

    const ctrl = Container.get(config.controllers.post.name) as IPostController;

    route.post(
        '',
        celebrate({
            body: Joi.object({
                idUser: Joi.string().required(),
                texto: Joi.string().required(),
                listTags: Joi.array().required(),
            }),
        }),
        (req, res, next) => ctrl.createPost(req, res, next),
    );

    route.delete('/:idUser', (req, res, next) => { ctrl.deleteUserPosts(req, res, next); req.params.idUser; } );

    route.get('/users/:idUser', (req, res, next) => { ctrl.getPostsConectionByUserId(req, res, next); req.params.idUser; } );

    route.get('/:idUser', (req, res, next) => { ctrl.getPostsByUserId(req, res, next); req.params.idUser; } );

    route.get('/ById/:idPost', (req, res, next) => { ctrl.getPostById(req, res, next); req.params.idPost; } );

    route.put('/gostos',celebrate({
        body: Joi.object({
            postId: Joi.string().required(),
            status: Joi.string().required(),
            userId: Joi.string().required(),
        }),
    }),
    (req, res, next) => ctrl.likeOrDislikePost(req, res, next),
    );
};
