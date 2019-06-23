import * as jsPDF from 'jspdf';

export class StaticUtilities {
  static pdfTextCenter(doc: jsPDF, text: string, fontsize: number) {
    const pageWidth = doc.internal.pageSize.width;
    const txtWidth =
      (doc.getStringUnitWidth(text) * fontsize) / doc.internal.scaleFactor;
    return (pageWidth - txtWidth) / 2;
  }
  static pdfTextRight(doc: jsPDF, text: string, fontsize: number) {
    const pageWidth = doc.internal.pageSize.width;
    const txtWidth =
      (doc.getStringUnitWidth(text) * fontsize) / doc.internal.scaleFactor;
    return pageWidth - txtWidth - 10;
  }

  //#region "setLocalDate"
  static setLocalDate(date: any) {
    if (date === null || date === undefined) {
      return null;
    } else {
      return new Date(
        new Date(date).getTime() - new Date().getTimezoneOffset() * 60000
      );
    }
  }
  //#endregion

  //#region "getLocalDate"
  static getLocalDate(date: any) {
    return new Date(
      new Date(date).getFullYear(),
      new Date(date).getMonth(),
      new Date(date).getDate(),
      new Date().getHours(),
      new Date().getMinutes(),
      new Date().getSeconds(),
      new Date().getMilliseconds()
    );
  }
  //#endregion
}
