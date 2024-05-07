import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { HttpServiceBase } from './http.base-service';
import { Injectable } from '@angular/core';
import { Entity } from '../utils/entity';
import { firstValueFrom, lastValueFrom } from 'rxjs';
import { ApiResponse } from '../utils/api.response';

@Injectable()
export class HttpService extends HttpServiceBase {

    constructor(private httpClient: HttpClient) {
        super();
    }
    override async get(url: string): Promise<Map<string, any>> {
        let result = await firstValueFrom(this.httpClient.get<ApiResponse>(url));
        const map = new Map<string, any>(Object.entries(result));
        return map;
    }

    override getAll(url: string): Promise<Map<string, any>[]> {
        return new Promise((resolve, reject) => {
            this.httpClient.get<ApiResponse[]>(url).subscribe({
                next: (response: ApiResponse[]) => {
                    const maps: Map<string, any>[] = [];
                    for (const json of response) {
                        let map = new Map<string, any>(Object.entries(json));
                        maps.push(map);
                    }
                    resolve(maps);
                },
                error: (error: HttpErrorResponse) => {
                    reject(error);
                },
            });
        });
    }

    override async post(url: string, data: Entity | HttpParams, headers?: HttpHeaders | undefined): Promise<Map<string, any>> {
        let body = data instanceof Entity ? data.toJson() : data;
        let result = await firstValueFrom(this.httpClient.post<ApiResponse>(url, body, { headers: headers }));
        let map = new Map<string, any>(Object.entries(result));
        return map;
    }
    override async put(url: string, data: Entity, headers?: HttpHeaders | undefined): Promise<Map<string, any>> {
        let body = data instanceof Entity ? data.toJson() : data;
        let result = await firstValueFrom(this.httpClient.put<ApiResponse>(url, data.toJson(), { headers: headers }));
        let map = new Map<string, any>(Object.entries(result));
        return map;
    }
    override async delete(url: string, headers?: HttpHeaders | undefined): Promise<Map<string, any>> {
        let result = await firstValueFrom(this.httpClient.delete<ApiResponse>(url, { headers: headers }));
        let map = new Map<string, any>(Object.entries(result));
        return map;
    }
}