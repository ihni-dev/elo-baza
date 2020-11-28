import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SubjectResult } from './subject.model';
import { SubjectService } from './subject.service';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrls: ['./subject.component.scss']
})
export class SubjectComponent implements OnInit {
  constructor(public subjectService: SubjectService) {}

  public subjectResult$: Observable<SubjectResult>;
  displayedColumns: string[] = ['key', 'name'];

  ngOnInit(): void {
    this.subjectResult$ = this.subjectService.getSubjects();
  }
}
