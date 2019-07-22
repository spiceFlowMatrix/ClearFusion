import { Injectable, Inject } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable()
export class AppSettingsService {

    constructor(@Inject('BASE_URL') private baseUrl: string) { }


    public getBaseUrl(): string {
        return environment.apiUrl;
    }
    public getDocUrl(): string {
        return environment.docUrl;
    }
    public getHubUrl(): string {
        return environment.hubUrl;
    }

    // private getApiURL(baseURL): string {
    //     return (baseURL.substring(baseURL.lastIndexOf('/')))
    // }
}

export const SETTINGS_PROVIDERS = [
    { provide: AppSettingsService, useClass: AppSettingsService }
];
