import { User } from "../../domain/entities/user.entity";

export class UserModel extends User {
    override toJson(): Map<string, any> {
        throw new Error("Method not implemented.");
    }

    static fromJson(data: Map<string, any>): User {
        throw new Error("Method not implemented.");
    }
}