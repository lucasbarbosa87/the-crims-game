import { Injectable } from "@angular/core";
import { User } from "../domain/entities/user.entity";
import { StorageService } from "../../core/services/storage-service";
import { Failure } from "../../core/utils/failure";

export abstract class UserSessionService {
    abstract getUser(): User;
    abstract clean(): void;
    abstract addUser(user: User): Promise<void | Failure>;
}

@Injectable()
export class UserSessionServiceImpl extends UserSessionService {
    private key: string = "user-session";
    constructor(private storageService: StorageService) {
        super();
    }

    override getUser(): User {
        let user: User = this.storageService.get(this.key);
        return user;
    }

    override clean(): void {
        return this.storageService.clean(this.key);
    }

    override addUser(user: User): Promise<void | Failure> {
        return this.storageService.save(this.key, user);
    }
}