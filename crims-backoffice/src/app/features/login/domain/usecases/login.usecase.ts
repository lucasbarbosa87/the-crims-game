import { Injectable } from "@angular/core";
import { LoginDto } from "../../data/dtos/login.dto";
import { UseCase } from "../../../../core/utils/usecase";
import { User } from "../../../../shared/domain/entities/user.entity";
import { AuthRepository } from "../../../../shared/domain/repositories/auth.repository";

@Injectable()
export class LoginUsecase implements UseCase<LoginDto, User> {

    constructor(private authRepository: AuthRepository) { }

    async execute(param: LoginDto): Promise<User> {
        return this.authRepository.doLogin(param.email, param.password);
    }

}