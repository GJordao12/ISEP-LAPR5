import {Service,Inject} from "typedi";

import {Document,FilterQuery,Model} from "mongoose";

import IComentarioRepo from "../services/IRepos/IComentarioRepo";
import {IComentarioPersistence} from "../dataschema/IComentarioPersistence";
import {ComentarioId} from "../domain/comentarioId";
import {Comentario} from "../domain/comentario";
import {ComentarioMap} from "../mappers/ComentarioMap";
import {PostId} from "../domain/postId";

@Service()
export default class ComentarioRepo implements IComentarioRepo{

    private models: any;

    constructor(@Inject('comentarioSchema') private comentarioSchema: Model<IComentarioPersistence & Document>) {}

    private createBaseQuery(): any {
        return{
            where: {},
        };
    }

    public async exists(comentario: Comentario): Promise<boolean>{

        // eslint-disable-next-line @typescript-eslint/no-angle-bracket-type-assertion
        const idX = comentario.id instanceof PostId ? (<PostId>comentario.id).id : comentario.id;

        const query = {domainId: idX};
        const postDocument = await this.comentarioSchema.findOne(query as FilterQuery<IComentarioPersistence & Document>);

        return !!postDocument === true;
    }

    public async save(comentario: Comentario): Promise<Comentario>{

        const query = {domainId: comentario.id.toString()};

        const comentarioDocument = await this.comentarioSchema.findOne(query);

        try{

            if(comentarioDocument === null){

                const rawUser: any = ComentarioMap.toPersistence(comentario);

                const comentarioCreated = await this.comentarioSchema.create(rawUser);

                return ComentarioMap.toDomain(comentarioCreated);
            }else{

                comentarioDocument.id = comentario.id.toString();
                comentarioDocument.idPost = comentario.idPost;
                comentarioDocument.idUser = comentario.idUser;
                comentarioDocument.texto = comentario.texto.props.value;
                comentarioDocument.listTags = comentario.listTags;
                comentarioDocument.likes = comentario.likes;
                comentarioDocument.dislikes = comentario.dislikes;
                comentarioDocument.data = comentario.data;

                await comentarioDocument.save();

                return comentario;
            }
        } catch (err){
            throw err;
        }
    }

    public async findByDomainId(comentarioId: ComentarioId | string): Promise<Comentario>{
        const query = {domainId: comentarioId};
        const comentarioRecord = await this.comentarioSchema.findOne(query as FilterQuery<IComentarioPersistence & Document>);

        if(comentarioRecord != null){
            return ComentarioMap.toDomain(comentarioRecord);
        }else return null;
    }

    public async deleteByUserId(userID: ComentarioId | string) {
        const query = { idUser: userID};
        await this.comentarioSchema.deleteMany(query as FilterQuery<IComentarioPersistence & Document>);
    }

}
