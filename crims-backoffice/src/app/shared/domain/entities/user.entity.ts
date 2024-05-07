import { Entity } from "../../../core/utils/entity";

export abstract class User extends Entity {

    constructor(
        public id: string,
        public nickname: string,
        public email: string,
        public firstName: string,
        public lastName: string,
        public accessToken: string,
        public refreshToken: string
    ) {
        super();
    }


}