import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfiguration } from './app-configuration.model';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppConfigurationService {
  private configuration: AppConfiguration = {
    apiUrl: ''
  };

  constructor(private http: HttpClient) {}

  public loadConfiguration(): Subscription {
    return this.http
      .get<AppConfiguration>('/assets/app-configuration.json')
      .subscribe((res) => (this.configuration = res));
  }

  public getConfig(): AppConfiguration {
    return this.configuration;
  }

  public combineWithPortfolioServiceApiUrl(relativePath: string): string {
    const apiUrl = this.getConfig().apiUrl;
    return apiUrl + relativePath;
  }
}
