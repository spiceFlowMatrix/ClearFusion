import { Injectable, Inject } from '@angular/core';

@Injectable()
export class AppSettingsService {

    constructor(@Inject('BASE_URL') private baseUrl: string) { }


    public getBaseUrl(): string {
        return '/api/';
        // return this.baseUrl + 'api/';
    }
    public getDocUrl(): string {
        return '/Docs/';
        // return this.baseUrl + 'Docs/';
    }
    public getHubUrl(): string {
        return '/chathub/';
        // return this.baseUrl+ 'chathub/';
    }

    // private getApiURL(baseURL): string {
    //     return (baseURL.substring(baseURL.lastIndexOf('/')))
    // }
}

export const SETTINGS_PROVIDERS = [
    { provide: AppSettingsService, useClass: AppSettingsService }
];
