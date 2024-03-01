import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ClassService } from '../../dashboard/class.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-student-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './student-form.component.html',
  styleUrl: './student-form.component.css',
})
export class StudentFormComponent implements OnInit {
  public studentFormGroup = new FormGroup({
    id: new FormControl(''),
    firstName: new FormControl('', Validators.required),
  });
  title?: string;
  classId?: number;
  studentList?: any[];
  // selectedStudents?: any[] = [];
  selectedStudents = [];

  constructor(
    public bsModalRef: BsModalRef,
    private classService: ClassService,
    private toast: ToastrService
  ) {}

  ngOnInit(): void {
    this.getList();
  }

  getList() {
    this.classService.getStudentsNotInClass(this.classId!).subscribe((res) => {
      if (res) {
        this.studentList = res;
      }
    });
  }

  save() {
    const arrayInitial = Object.keys('studentId').filter((key: any) => {
      return this.selectedStudents[key];
    });

    let body = [];

    for (let i = 0; i < arrayInitial.length; i++) {
      body.push({
        studentId: arrayInitial[i],
      });
    }

    this.classService
      .addStudentsToClass(this.classId!, body)
      .subscribe((res) => {
        this.toast.success('Success', 'Students added to class');
        this.bsModalRef.hide();
      });
  }

  cancel() {
    this.bsModalRef.hide();
  }
}
