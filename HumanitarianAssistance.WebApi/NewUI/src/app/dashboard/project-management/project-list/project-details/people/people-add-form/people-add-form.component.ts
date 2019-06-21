import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IUserListModel, IAddPeoplePermissionModel } from '../../models/project-details.model';

@Component({
  selector: 'app-people-add-form',
  templateUrl: './people-add-form.component.html',
  styleUrls: ['./people-add-form.component.scss']
})
export class PeopleAddFormComponent implements OnInit {

  @Input() userList: IUserListModel[] = [];
  @Input() roleList: any[] = [];
  @Output() addForm = new EventEmitter<any>();

  addPeopleForm: FormGroup;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.addPeopleForm = this.fb.group({
      userId: [null, Validators.required],
      roleId: [null, Validators.required]
    });
  }

  onAddFormSubmit(data: any) {
    const model: IAddPeoplePermissionModel = {
      UserId: data.userId,
      RoleId: data.roleId
    };
    this.addForm.emit(model);
  }

}
