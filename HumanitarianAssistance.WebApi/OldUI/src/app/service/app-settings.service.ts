import { Injectable, Inject } from '@angular/core';

@Injectable()
export class AppSettingsService {

    constructor(@Inject('BASE_URL') private baseUrl: string) { }


    public getBaseUrl(): string {
        return this.baseUrl + 'api/';
    }
    public getDocUrl(): string {
        return this.baseUrl + 'Docs/';
    }
    public getHubUrl(): string {
        return this.baseUrl + 'chathub/';
    }
}

export const SETTINGS_PROVIDERS = [
    { provide: AppSettingsService, useClass: AppSettingsService }
];
