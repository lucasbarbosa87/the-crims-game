import { Injectable } from "@angular/core";
import { Failure } from "../../../core/utils/failure";
import { UseCase } from "../../../core/utils/usecase";
import { User } from "../entities/user.entity";
import { UserSessionService } from "../../services/user-session.service";

@Injectable()
export class GetUserUsecase implements UseCase<undefined, User> {

    constructor(private userSessionService: UserSessionService) { }

    async execute(param?: undefined): Promise<User> {
        return this.userSessionService.getUser();
    }

}