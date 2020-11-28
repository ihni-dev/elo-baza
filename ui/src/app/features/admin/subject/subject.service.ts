import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfigurationService } from '../../../core/app-configuration/app-configuration.service';
import { Observable } from 'rxjs';
import { delay } from 'rxjs/operators';
import { SubjectResult } from './subject.model';

@Injectable()
export class SubjectService {
  constructor(
    private httpClient: HttpClient,
    private appConfiguration: AppConfigurationService
  ) {}

  private subjectUrl = this.appConfiguration.combineWithApiUrl('subject');

  public getSubjects(): Observable<SubjectResult> {
    return this.httpClient.get<SubjectResult>(this.subjectUrl).pipe(delay(400));
  }
}
