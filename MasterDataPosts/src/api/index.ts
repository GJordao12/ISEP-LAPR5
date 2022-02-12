import { Router } from 'express';
import post from './routes/postRoute';
import comentario from './routes/comentarioRoute';

export default () => {
    const app = Router();

    post(app);
    comentario(app);

    return app;
};
