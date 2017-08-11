using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace XscpSys.Controllers
{
    public class Reflection
    {
        #region 属性
        /// <summary>
        /// 获取属性信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        {
            PropertyInfo propertyInfo = type.GetProperty(propertyName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static);
            return propertyInfo;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(Type type, object obj, string propertyName)
        {
            PropertyInfo propertyInfo = Reflection.GetPropertyInfo(type, propertyName);
            return propertyInfo.GetValue(obj, null);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(Type type, object obj, string propertyName, object value)
        {
            PropertyInfo propertyInfo = Reflection.GetPropertyInfo(type, propertyName);
            propertyInfo.SetValue(obj, value,null);
        }

        #endregion

        #region 字段
        /// <summary>
        /// 获取字段信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static FieldInfo GetFieldInfo(Type type, string fieldName)
        {
            FieldInfo fieldInfo = type.GetField(fieldName,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static);
            return fieldInfo;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object GetFieldValue(Type type, object obj, string fieldName)
        {
            FieldInfo fieldInfo = Reflection.GetFieldInfo(type, fieldName);
            return fieldInfo.GetValue(obj);
        }


        /// <summary>
        /// 设置字段值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public static void SetFieldValue(Type type, object obj, string fieldName, object value)
        {
            FieldInfo fieldInfo = Reflection.GetFieldInfo(type, fieldName);
            fieldInfo.SetValue(obj, value);
        }
        #endregion
    }
}
