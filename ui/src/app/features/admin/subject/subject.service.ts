import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
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

  public getSubjects(
    page: number,
    pageSize: number,
    subjectName: string
  ): Observable<SubjectResult> {
    let httpParams = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    if (subjectName) httpParams = httpParams.set('name', subjectName);

    return this.httpClient
      .get<SubjectResult>(this.subjectUrl, {
        params: httpParams
      })
      .pipe(delay(400));
  }
}
