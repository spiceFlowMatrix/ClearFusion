import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GoodsRecievedUploadComponent } from './goods-recieved-upload.component';

describe('GoodsRecievedUploadComponent', () => {
  let component: GoodsRecievedUploadComponent;
  let fixture: ComponentFixture<GoodsRecievedUploadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GoodsRecievedUploadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GoodsRecievedUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
