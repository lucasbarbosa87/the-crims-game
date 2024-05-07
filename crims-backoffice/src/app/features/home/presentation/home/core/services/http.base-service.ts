import { HttpHeaders, HttpParams } from "@angular/common/http";
import { Entity } from "../utils/entity";

export abstract class HttpServiceBase {
    abstract get(url: string): Promise<Map<string, any>>;
    abstract getAll(url: string): Promise<Map<string, any>[]>;
    abstract post(url: String, data: Entity | HttpParams, headers?: HttpHeaders,): Promise<Map<string, any>>;
    abstract put(url: String, data: Entity, headers?: HttpHeaders,): Promise<Map<string, any>>;
    abstract delete(url: String, headers?: HttpHeaders): Promise<Map<string, any>>;
}