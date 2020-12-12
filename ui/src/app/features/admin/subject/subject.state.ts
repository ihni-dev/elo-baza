import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { Observable } from 'rxjs';
import { finalize, tap } from 'rxjs/operators';
import { SubjectResult } from './subject.model';
import { SubjectService } from './subject.service';

export class GetAllSubjects {
  static readonly type = '[Subject] GetAll';

  constructor(
    public page: number,
    public pageSize: number,
    public name: string = ''
  ) {}
}

export class ClearSubjects {
  static readonly type = '[Subject] Clear';
}

export class SubjectStateModel {
  subjectResult: SubjectResult;
  areSubjectsLoading: boolean;
}

@Injectable()
@State<SubjectStateModel>({
  name: 'subjects',
  defaults: {
    subjectResult: SubjectResult.empty(),
    areSubjectsLoading: false
  }
})
export class SubjectState {
  constructor(private subjectService: SubjectService) {}

  @Selector()
  static getSubjectResult(state: SubjectStateModel): SubjectResult {
    return state.subjectResult;
  }

  @Selector()
  static areSubjectsLoading(state: SubjectStateModel): boolean {
    return state.areSubjectsLoading;
  }

  @Action(GetAllSubjects, { cancelUncompleted: true })
  getAllSubjects(
    ctx: StateContext<SubjectStateModel>,
    action: GetAllSubjects
  ): Observable<SubjectResult> {
    ctx.patchState({
      areSubjectsLoading: true
    });

    return this.subjectService
      .getSubjects(action.page, action.pageSize, action.name)
      .pipe(
        tap({
          next: (result) => {
            ctx.patchState({
              subjectResult: result
            });
          },
          error: (err) => {
            console.error(err);
          }
        }),
        finalize(() => {
          ctx.patchState({
            areSubjectsLoading: false
          });
        })
      );
  }

  @Action(ClearSubjects)
  clearSubjects(ctx: StateContext<SubjectStateModel>): void {
    ctx.patchState({
      subjectResult: SubjectResult.empty()
    });
  }
}
