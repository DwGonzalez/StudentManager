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
import { HttpClientModule } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-class-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './class-form.component.html',
  styleUrl: './class-form.component.css',
})
export class ClassFormComponent implements OnInit {
  title?: string;
  idClass?: number;
  closeBtnName?: string;
  isCreate: boolean = false;
  isEdit: boolean = false;
  public classGroup = new FormGroup({
    id: new FormControl(''),
    name: new FormControl('', Validators.required),
  });

  constructor(
    public bsModalRef: BsModalRef,
    private classService: ClassService,
    private toast: ToastrService
  ) {}

  ngOnInit() {
    this.getClassDetail(this.idClass!);
  }

  getClassDetail(id: number) {
    if (id)
      this.classService.getClassById(id).subscribe((res) => {
        console.log(res);
        this.classGroup.setValue(res);
      });
  }

  save() {
    if (this.isCreate) {
      console.log(this.classGroup.value);
      this.classService.createNewClass(this.classGroup.value).subscribe(() => {
        this.toast.success('Success', 'Class saved succesfully 👍🏽');
        this.bsModalRef.hide();
      });
    }

    if (this.isEdit)
      this.classService
        .updateClass(this.idClass!, this.classGroup.value)
        .subscribe(() => {
          this.toast.success('Success', 'Class edited successfully 👍🏽');
          this.bsModalRef.hide();
        });
  }

  cancel() {
    this.bsModalRef.hide();
  }
}
