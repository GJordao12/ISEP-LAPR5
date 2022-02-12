import { ValueObject } from '../core/domain/ValueObject';
import { Result } from '../core/logic/Result';
import { Guard } from '../core/logic/Guard';

interface TextoPostProps {
    value: string;
}

export class TextoPost extends ValueObject<TextoPostProps> {
    get value(): string {
        return this.props.value;
    }

    public constructor(props: TextoPostProps) {
        super(props);
    }

    public static create(textoPost: string): Result<TextoPost> {
        const guardResult = Guard.againstNullOrUndefined(textoPost, 'textoPost');
        if (!guardResult.succeeded || textoPost.length > 10000) {
            return Result.fail<TextoPost>(guardResult.message);
        } else {
            return Result.ok<TextoPost>(new TextoPost({ value: textoPost }));
        }
    }
}
