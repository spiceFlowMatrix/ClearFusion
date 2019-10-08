import {FileSourceEntityTypes } from '../enums';

 export class SignedUrlObjectName {

  static getSignedURLObjectName(pageId: number, entityId?: number): string {
    let objectName = '';

    switch (pageId) {
      case FileSourceEntityTypes.Voucher:
      objectName = 'voucher/';
      break;
      case FileSourceEntityTypes.StorePurchase:
      objectName = 'storepurchase/' + entityId + '/';
      break;
      case FileSourceEntityTypes.Employee:
      objectName = 'employeeprofileimage/' + entityId + '/';
    }

    return objectName;
  }
 }
