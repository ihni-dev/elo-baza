import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { SubjectResult } from './subject.model';
import { GetAllSubjects, SubjectState } from './subject.state';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.scss']
})
export class SubjectComponent implements OnInit {
  paginatorPageSizes = [5, 10, 25];
  paginatorPageIndex = 1;
  paginatorPageSize = this.paginatorPageSizes[0];

  @Select(SubjectState.getSubjectResult)
  subjectResult$: Observable<SubjectResult>;

  @Select(SubjectState.areSubjectsLoading)
  areSubjectsLoading$: Observable<boolean>;

  constructor(private store: Store) {}

  ngOnInit(): void {
    this.store.dispatch(
      new GetAllSubjects(this.paginatorPageIndex, this.paginatorPageSize)
    );
  }

  onPaginationChange($event: PageEvent): void {
    this.paginatorPageIndex = $event.pageIndex;
    this.paginatorPageSize = $event.pageSize;

    this.store.dispatch(
      new GetAllSubjects(this.paginatorPageIndex + 1, this.paginatorPageSize)
    );
  }
}
