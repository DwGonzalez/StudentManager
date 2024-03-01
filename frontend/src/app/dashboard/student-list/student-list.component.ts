import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ClassService } from '../class.service';
import { CommonModule } from '@angular/common';

import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import Swal from 'sweetalert2';
import { ToastrService } from 'ngx-toastr';
import { ScoreFormComponent } from '../../components/score-form/score-form.component';
import { LiteralGradePipe } from '../../pipes/literal-grade.pipe';
import { StudentFormComponent } from '../../components/student-form/student-form.component';

import { NgArrayPipesModule } from 'ngx-pipes';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-student-list',
  standalone: true,
  templateUrl: './student-list.component.html',
  styleUrl: './student-list.component.css',
  providers: [BsModalService],
  imports: [
    RouterModule,
    CommonModule,
    FormsModule,
    LiteralGradePipe,
    NgArrayPipesModule,
    NgxPaginationModule,
  ],
})
export class StudentListComponent implements OnInit {
  title: string = 'Student List';
  subjectTitle!: string;
  classId!: number;
  loading: boolean = true;
  students!: Array<any>;
  studentsOrdered!: Array<any>;
  scoreToShow = 'F';
  studentFilter: string = '';
  page = 1;

  modalRef?: BsModalRef;
  modalConfig = {
    backdrop: true,
    ignoreBackdropClick: false,
    class: 'modal-dialog-centered',
  };

  constructor(
    private route: ActivatedRoute,
    private classService: ClassService,
    private modalService: BsModalService,
    private toast: ToastrService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.classId = params['id'];
      if (!!this.classId) {
        this.getClassDetail(this.classId);
      }
    });
  }

  getClassDetail(id: number) {
    this.classService.getClassById(this.classId).subscribe(
      (res: any) => {
        this.subjectTitle = res.name;
        this.getStudentsFromClass(id);
        this.loading = false;
      },
      (err) => {
        console.log(err);
      }
    );
  }

  getStudentsFromClass(id: number) {
    this.classService.getStudentsClass(this.classId).subscribe(
      (res: any) => {
        this.students = res;
      },
      (err) => {
        console.log(err);
      }
    );
  }

  addScoreModal(
    template: TemplateRef<void>,
    classId: number,
    score: number,
    studentId: number
  ) {
    const initialState: ModalOptions = {
      backdrop: true,
      ignoreBackdropClick: true,
      class: 'modal-dialog-centered',
      initialState: {
        title: 'Assign Score',
        request: {
          score: score,
          classId: classId,
          studentId: studentId,
        },
      },
    };
    this.modalRef = this.modalService.show(ScoreFormComponent, initialState);
    this.modalRef.onHide?.subscribe(() => {
      this.getClassDetail(classId);
    });
    this.modalRef.content.isCreate = true;
    this.modalRef.content.closeBtnName = 'Save';
  }

  addStudentToClassModal(template: TemplateRef<void>, classId: number) {
    const initialState: ModalOptions = {
      backdrop: true,
      ignoreBackdropClick: true,
      class: 'modal-dialog-centered modal-lg',
      initialState: {
        title: 'Add Students',
        classId: classId,
      },
    };
    this.modalRef = this.modalService.show(StudentFormComponent, initialState);
    this.modalRef.onHide?.subscribe(() => {
      this.getClassDetail(classId);
    });
    this.modalRef.content.isCreate = true;
    this.modalRef.content.closeBtnName = 'Save';
  }

  removeFromClass(classId: number, studentId: number) {
    Swal.fire({
      title: 'Are you sure you want to remove this student from this class?',
      showCancelButton: true,
      confirmButtonText: 'Yes',
      denyButtonText: 'No',
    }).then((res) => {
      if (res.isConfirmed) {
        this.classService
          .removeStudentFromClass(classId, studentId)
          .subscribe(() => {
            this.toast.success('Success', 'Student removed from class');
            this.getClassDetail(classId);
          });
      }
    });
  }

  closeModal() {}
}
