import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IComentarioController from "../../controllers/IControllers/IComentarioController";

import config from '../../../config';

const route = Router();

export default (app: Router) => {
    app.use('/comentario',route);

    const ctrl = Container.get(config.controllers.comentario.name) as IComentarioController;

    route.post(
        '',
        celebrate({
            body: Joi.object({
                idUser: Joi.string().required(),
                idPost: Joi.string().required(),
                texto: Joi.string().required(),
                listTags: Joi.array().required(),
            }),
        }),
        (req,res,next) => ctrl.createComentario(req,res,next),
    );

    route.delete('/:idUser', (req, res, next) => { ctrl.deleteUserComments(req, res, next); req.params.idUser; } );


    route.put('/gostos',celebrate({
        body: Joi.object({
            comentarioId: Joi.string().required(),
            status: Joi.string().required(),
            userId: Joi.string().required(),
        }),
    }),
    (req, res, next) => ctrl.likeOrDislikeComentario(req, res, next),
    );

}
