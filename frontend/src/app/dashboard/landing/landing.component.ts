import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ClassService } from '../class.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { ClassFormComponent } from '../../components/class-form/class-form.component';
import { HttpClientModule } from '@angular/common/http';

import Swal from 'sweetalert2';
import { ToastrService } from 'ngx-toastr';
import { NgArrayPipesModule } from 'ngx-pipes';
import { NgxPaginationModule } from 'ngx-pagination';
import { AttendanceListComponent } from '../../components/attendance-list/attendance-list.component';

@Component({
  selector: 'app-landing',
  standalone: true,
  templateUrl: './landing.component.html',
  styleUrl: './landing.component.css',
  providers: [BsModalService],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    NgArrayPipesModule,
    NgxPaginationModule,
  ],
})
export class LandingComponent implements OnInit {
  modalRef?: BsModalRef;
  modalConfig = {
    backdrop: true,
    ignoreBackdropClick: false,
    class: 'modal-dialog-centered',
  };

  loading: boolean = true;
  classes!: Array<any>;
  classesListTitle: string = 'Classes List';
  classFilter: string = '';
  page = 1;

  constructor(
    private classService: ClassService,
    private modalService: BsModalService,
    private toast: ToastrService
  ) {}

  ngOnInit() {
    this.getAllClasses();
  }

  getAllClasses() {
    this.classService.getAllClasses().subscribe((res: any) => {
      this.classes = res;
      this.loading = false;
    });
  }

  deleteClass(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete?',
      showCancelButton: true,
      confirmButtonText: 'Yes',
      denyButtonText: 'No',
    }).then((res) => {
      if (res.isConfirmed) {
        this.classService.deleteClass(id).subscribe(() => {
          this.toast.success('Success', 'Class deleted');
          this.getAllClasses();
        });
      }
    });
  }

  addClassModal(template: TemplateRef<void>) {
    const initialState: ModalOptions = {
      backdrop: true,
      ignoreBackdropClick: true,
      class: 'modal-dialog-centered',
      initialState: {
        title: 'Add Class',
      },
    };
    this.modalRef = this.modalService.show(ClassFormComponent, initialState);
    this.modalRef.onHide?.subscribe(() => {
      this.getAllClasses();
    });
    this.modalRef.content.isCreate = true;
    this.modalRef.content.closeBtnName = 'Save';
  }

  editModal(template: TemplateRef<void>, id: number) {
    const initialState: ModalOptions = {
      backdrop: true,
      ignoreBackdropClick: true,
      class: 'modal-dialog-centered',
      initialState: {
        title: 'Edit Class',
        idClass: id,
      },
    };
    this.modalRef = this.modalService.show(ClassFormComponent, initialState);
    this.modalRef.content.isEdit = true;
    this.modalRef.content.closeBtnName = 'Save';
    this.modalService.onHide.subscribe(() => {
      console.log('saved too');
      this.getAllClasses();
    });
  }

  attendanceListModal(template: TemplateRef<void>, classId: number) {
    const initialState: ModalOptions = {
      backdrop: true,
      ignoreBackdropClick: true,
      class: 'modal-dialog-centered modal-lg',
      initialState: {
        title: 'Attendance List',
        classId: classId,
      },
    };
    this.modalRef = this.modalService.show(
      AttendanceListComponent,
      initialState
    );
  }

  openModal2(template: TemplateRef<void>) {
    this.modalRef = this.modalService.show(template, this.modalConfig);
  }

  closeModal() {
    this.modalRef?.hide();
    this.getAllClasses();
  }
}
