import { Injectable } from "@angular/core";
import { Failure, UnhadledFailure } from "../utils/failure";

export abstract class StorageService {
    abstract clean(key: string): void;
    abstract save(key: string, data: any): Promise<void>;
    abstract get(key: string): any;
}

@Injectable()
export class StorageServiceImpl extends StorageService {
    override clean(key: string): void {
        window.sessionStorage.removeItem(key);
    }

    override save(key: string, data: any): Promise<void> {
        return new Promise((resolver, reject) => {
            try {
                window.sessionStorage.setItem(key, JSON.stringify(data));
                resolver();
                return;
            } catch (ex: any) {
                reject(ex.message);
            }
        });
    }

    override get(key: string): any {
        return window.sessionStorage.getItem(key);
    }

}