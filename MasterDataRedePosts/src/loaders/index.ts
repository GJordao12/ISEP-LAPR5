import expressLoader from './express';
import dependencyInjectorLoader from './dependencyInjector';
import mongooseLoader from './mongoose';
import Logger from './logger';

import config from '../../config';

export default async ({ expressApp }) => {
    const mongoConnection = await mongooseLoader();
    Logger.info('✌️ DB loaded and connected!');


    const postSchema = {
    // compare with the approach followed in repos and services
        name: 'postSchema',
        schema: '../persistence/schemas/postSchema',
    };

    const comentarioSchema = {
        name: 'comentarioSchema',
        schema: '../persistence/schemas/comentarioSchema',
    };



    const postController = {
        name: config.controllers.post.name,
        path: config.controllers.post.path,
    };

    const comentarioController = {
        name: config.controllers.comentario.name,
        path: config.controllers.comentario.path,
    };



    const postRepo = {
        name: config.repos.post.name,
        path: config.repos.post.path,
    };

    const comentarioRepo = {
        name: config.repos.comentario.name,
        path: config.repos.comentario.path,
    };


    const postService = {
        name: config.services.post.name,
        path: config.services.post.path,
    };

    const comentarioService = {
        name: config.services.comentario.name,
        path: config.services.comentario.path,
    };

    await dependencyInjectorLoader({
        mongoConnection,
        schemas: [postSchema,comentarioSchema],
        controllers: [postController,comentarioController],
        repos: [ postRepo, comentarioRepo],
        services: [ postService, comentarioService],
    });
    Logger.info('✌️ Schemas, Controllers, Repositories, Services, etc. loaded');

    await expressLoader({ app: expressApp });
    Logger.info('✌️ Express loaded');
};
