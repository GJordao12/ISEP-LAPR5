import {IComentarioPersistence} from "../../dataschema/IComentarioPersistence";
import mongoose from "mongoose";

const Comentario = new mongoose.Schema(

    {
        domainId:{
            type: String,
            unique: true
        },

        idPost:{
            type: String,
            index: true
        },

        idUser: {
            type: String,
            index: true,
        },

        texto: {
            type: String,
            index: true,
        },

        listTags:{
            type: Array,
            index: true
        },

        likes:{
            type: Array,
            index: true
        },

        dislikes:{
            type: Array,
            index: true
        },

        data:{
            type: Date,
            index: true
        }
    },
    {timestamps: true}

);

export default mongoose.model<IComentarioPersistence & mongoose.Document>("Comentario", Comentario);
