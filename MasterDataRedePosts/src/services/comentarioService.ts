import {Service,Inject} from "typedi";
import config from "../../config";
import IComentarioDTO from "../dto/IComentarioDTO";
import { Comentario} from "../domain/comentario";
import IComentarioRepo from "./IRepos/IComentarioRepo";
import {Result} from "../core/logic/Result";
import {ComentarioMap} from "../mappers/ComentarioMap";
import IComentarioService from "./IServices/IComentarioService";
import https = require('https'); import fetch = require('node-fetch');

@Service()
export default class ComentarioService implements IComentarioService {

    constructor(@Inject(config.repos.comentario.name) private comentarioRepo: IComentarioRepo) {}

    httpsAgent = new https.Agent({
        rejectUnauthorized: false,
    });

    public async getComentario(comentarioID: string): Promise<Result<IComentarioDTO>>{
        try{
            const comentario = await this.comentarioRepo.findByDomainId(comentarioID);

            if (comentario === null){
                return Result.fail<IComentarioDTO>('Comentario not found');
            }
            else{
                const comentarioDTOResult = ComentarioMap.toDTO(comentario) as IComentarioDTO;
                return Result.ok<IComentarioDTO>(comentarioDTOResult);
            }
        }catch (e){
            throw e;
        }
    }

    public async deleteUserComments(userId: string) {
        try {
            await this.comentarioRepo.deleteByUserId(userId);
        } catch (e) {
            throw e;
        }
    }

    public async createComentario(comentarioDTO: IComentarioDTO): Promise<Result<IComentarioDTO>>{
        try{
            const comentarioOrError = await Comentario.create(comentarioDTO);

            if(comentarioOrError.isFailure){
                return Result.fail<IComentarioDTO>(comentarioOrError.errorValue());
            }

            const comentarioResult = comentarioOrError.getValue();

            await this.comentarioRepo.save(comentarioResult);

            const comentarioDTOResult = ComentarioMap.toDTO(comentarioResult) as IComentarioDTO;
            return Result.ok<IComentarioDTO>(comentarioDTOResult);
        }
        catch (e){
            throw e;
        }
    }

    public async updateTags(comentario: IComentarioDTO): Promise<IComentarioDTO>{
        const tags = new Array<string>();
        for (const name of comentario.listTags) {
            const response = await fetch("https://localhost:5001/api/Tag/AddTags/".concat(name), {
                method: 'PUT',
                agent: this.httpsAgent,

            });
            const data = await response.json();
            tags.push(data.id);
        }

        comentario.listTags = tags;
        return comentario;
    }

    public async updateLikeOrDislike(comentarioId: string, status: string, userId: string): Promise<Result<IComentarioDTO>>
    {
        try{
            const comentario = await this.comentarioRepo.findByDomainId(comentarioId);
            if(comentario === null){
                return Result.fail<IComentarioDTO>('Comment not found');
            }else{
                if(status === 'like'){
                    if(!comentario.likes.includes(userId) && !comentario.dislikes.includes(userId)){
                        comentario.likes.push(userId);
                    }
                }
                else if(status === 'dislike'){
                    if(!comentario.likes.includes(userId) && !comentario.dislikes.includes(userId)){
                        comentario.dislikes.push(userId);
                    }
                }
                await this.comentarioRepo.save(comentario);
                const comentarioDTOResult = ComentarioMap.toDTO(comentario) as IComentarioDTO;
                return Result.ok<IComentarioDTO>(comentarioDTOResult);
            }
        }catch (e){
            throw (e);
        }
    }

}



