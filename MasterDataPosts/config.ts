import dotenv from 'dotenv';

// Set the NODE_ENV to 'development' by default
process.env.NODE_ENV = process.env.NODE_ENV || 'development';

const envFound = dotenv.config();
if (!envFound) {
    // This error should crash whole process

    throw new Error("⚠️  Couldn't find .env file  ⚠️");
}

export default {
    /**
   * Your favorite port
   */
    port: parseInt(process.env.PORT, 10) || 5001,

    /**
   * That long string from mlab
   */
    databaseURL:
    process.env.MONGODB_URI ||
    '',

    /**
   * Your secret sauce
   */
    jwtSecret: process.env.JWT_SECRET || '',

    /**
   * Used by winston logger
   */
    logs: {
        level: process.env.LOG_LEVEL || 'info',
    },

    /**
   * API configs
   */
    api: {
        prefix: '/posts',
    },

    controllers: {
        post: {
            name: 'PostController',
            path: '../controllers/postController',
        },
        comentario: {
            name: 'ComentarioController',
            path: '../controllers/comentarioController',
        },
    },

    repos: {
        post: {
            name: 'PostRepo',
            path: '../repos/postRepo',
        },
        comentario: {
            name: 'ComentarioRepo',
            path: '../repos/comentarioRepo',
        },
    },

    services: {
        post: {
            name: 'PostService',
            path: '../services/postService',
        },
        comentario: {
            name: 'ComentarioService',
            path: '../services/comentarioService',
        },
    },
};
