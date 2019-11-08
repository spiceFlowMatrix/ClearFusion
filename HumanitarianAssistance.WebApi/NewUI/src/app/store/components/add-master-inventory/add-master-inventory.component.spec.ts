import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMasterInventoryComponent } from './add-master-inventory.component';

describe('AddMasterInventoryComponent', () => {
  let component: AddMasterInventoryComponent;
  let fixture: ComponentFixture<AddMasterInventoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddMasterInventoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMasterInventoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
