import { IPostPersistence } from "../../dataschema/IPostPersistence";
import mongoose from "mongoose";

const Post = new mongoose.Schema(
    {
        domainId: {
            type: String,
            unique: true
        },

        idUser: {
            type: String,
            index: true,
        },

        likes: {
            type: Array,
          index: true,
        },
        dislikes: {
            type: Array,
          index: true,
        },

        texto: {
            type: String,
            index: true,
        },
        listTags: {
            type: Array,
            index: true
        },
        data: {
            type: Date,
            index: true
        },
        listComentarios:{
            type: Array,
            index:true
        },
    },
    { timestamps: true }
);

export default mongoose.model<IPostPersistence & mongoose.Document>("Post", Post);
