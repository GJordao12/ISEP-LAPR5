import {ValueObject} from "../core/domain/ValueObject";
import {Result} from "../core/logic/Result";
import {Guard} from "../core/logic/Guard";

interface TextoComentarioProps {
    value: string;
}

export class TextoComentario extends ValueObject<TextoComentarioProps> {
    get value(): string {
        return this.props.value;
    }

    public constructor(props: TextoComentarioProps) {
        super(props);
    }

    public static create(textoComentario: string): Result<TextoComentario> {
        const guardResult = Guard.againstNullOrUndefined(textoComentario, 'textoComentario');

        if (!guardResult.succeeded || textoComentario.length > 1000) {
            return Result.fail<TextoComentario>(guardResult.message);
        } else {
            return Result.ok<TextoComentario>(new TextoComentario({value: textoComentario}));
        }
    }

}
