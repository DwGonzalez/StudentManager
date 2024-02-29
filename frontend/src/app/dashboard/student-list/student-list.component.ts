import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ClassService } from '../class.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './student-list.component.html',
  styleUrl: './student-list.component.css',
})
export class StudentListComponent implements OnInit {
  title: string = 'Student List';
  subjectTitle!: string;
  classId!: number;
  loading: boolean = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private classService: ClassService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.classId = params['id'];
      if (!!this.classId) {
        this.getClassDetail(this.classId);
        this.loading = false;
      }
    });
  }

  getClassDetail(id: number) {
    this.classService.getClassById(this.classId).subscribe(
      (res: any) => {
        this.subjectTitle = res.name;
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
