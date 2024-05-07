export abstract class Failure {
    constructor(public message: string) { }
}

export class UnhadledFailure extends Failure { }

export class NotFoundFailure extends Failure {
    constructor() {
        super('No se pudo encontrar el recurso buscado');
    }
}

export class BadRequestFailure extends Failure {
    constructor() {
        super('Esta realización una operación de manera incorrecta');
    }
}