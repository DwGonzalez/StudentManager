import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { ClassService } from '../class.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { ClassFormComponent } from '../../components/class-form/class-form.component';
import { HttpClientModule } from '@angular/common/http';

import Swal from 'sweetalert2';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [CommonModule, RouterModule, HttpClientModule],
  templateUrl: './landing.component.html',
  styleUrl: './landing.component.css',
  providers: [BsModalService],
})
export class LandingComponent implements OnInit {
  isModelOpen = false;
  modalRef?: BsModalRef;
  modalConfig = {
    backdrop: true,
    ignoreBackdropClick: false,
    class: 'modal-dialog-centered',
  };

  loading: boolean = true;
  filter: FormControl;
  classes!: Array<any>;
  classesListTitle: string = 'Classes List';

  constructor(
    private fb: FormBuilder,
    private classService: ClassService,
    private modalService: BsModalService,
    private toast: ToastrService
  ) {
    this.filter = this.fb.control('', { nonNullable: true });
  }

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

  openModal2(template: TemplateRef<void>) {
    this.modalRef = this.modalService.show(template, this.modalConfig);
  }

  closeModal() {
    this.modalRef?.hide();
    this.getAllClasses();
  }
}
